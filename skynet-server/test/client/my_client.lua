package.cpath = "lib/skynet/luaclib/?.so"
package.path = "lib/skynet/lualib/?.lua;service/?.lua"

local proto = require "proto"
local sproto = require "sproto"
local socket = require "client.socket"

local host = sproto.new(proto.s2c):host "package"
local request = host:attach(sproto.new(proto.c2s))

local fd = assert(socket.connect("127.0.0.1", 8840))
local session = 0

-- print(package .. " " .. string.len(package));

local function send_request(name, args)
    session = session + 1
    local str = request(name, args, session)
    local package = string.pack(">s2", str)
    print(str)
    socket.send(fd, package)
end

send_request("handshake")
-- send_request("set", {what = "hello", value = "123"});
-- send_request("get", {what = "hello"});
while true do
    -- local r = socket.recv(fd)
    -- if r then
    --     print("recv ", r)
    -- end

    local str = socket.readstdin()
    socket.usleep(100);
end
