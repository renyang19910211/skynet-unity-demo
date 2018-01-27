local gateserver = require "snax.gateserver"
local netpack = require "skynet.netpack"
local skynet = require "skynet"

local handler = {}
local connection = {}	-- fd -> connection : { fd , client, agent , ip, mode }
local forwarding = {}	-- agent -> connection

local CMD = {}
local watchdog

skynet.register_protocol {
	name = "client",
	id = skynet.PTYPE_CLIENT,
}

local function unforward(c)
	if c.agent then
		forwarding[c.agent] = nil
		c.agent = nil
		c.client = nil
	end
end

local function close_fd(fd)
	local c = connection[fd]
	if c then
		unforward(c)
		connection[fd] = nil
	end
end

function handler.connect(fd, ipaddr)
  local c = {
    fd = fd,
    ip = ipaddr,
  }
  connection[fd] = c
  skynet.send(watchdog, "lua", "socket", "open", fd, ipaddr)

  print("on connect " .. fd .. "  " .. ipaddr);
end

function handler.message(fd, msg, sz)
	print("rev msg " .. fd .. "  " .. sz);
  	local c = connection[fd]
	local agent = c.agent
	if agent then
		skynet.redirect(agent, c.client, "client", 1, msg, sz)
	else
		skynet.send(watchdog, "lua", "socket", "data", fd, netpack.tostring(msg, sz))
	end

end

function handler.disconnect(fd)
  print(fd .. " is disconnect");
end

function handler.error(fd, msg)
  print(fd .. " error " .. msg);
end

function handler.command(cmd, source, ...)
	local f = assert(CMD[cmd])
	return f(source, ...)
end

function handler.open(source, conf)
	watchdog = conf.watchdog or source
end

function handler.error(fd, msg)
	close_fd(fd)
	skynet.send(watchdog, "lua", "socket", "error", fd, msg)
end

function handler.warning(fd, size)
	skynet.send(watchdog, "lua", "socket", "warning", fd, size)
end

--允许客户端接收消息, agent调用
function CMD.forward(source, fd, client, address)
	local c = assert(connection[fd])
	unforward(c)
	c.client = client or 0
	c.agent = address or source
	forwarding[c.agent] = c
	gateserver.openclient(fd)
end

function CMD.accept(source, fd)
	local c = assert(connection[fd])
	unforward(c)
	gateserver.openclient(fd)
end

function CMD.kick(source, fd)
	gateserver.closeclient(fd)
end

gateserver.start(handler)
