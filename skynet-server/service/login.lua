local skynet = require "skynet"
local CMD = {}

function CMD.register_account(args)
    print("regitster ", args.account)
    local result = skynet.call("database", "lua", "create", args.account, args.password)
    return {msg = result}
end

function CMD.login_account(args)
	print("login ", args.account)
	local result = skynet.call("database", "lua", "get_db_account", args.account, args.password)
	if result then
        local ret = {result = (result.account == args.account) and (result.password == args.password)}
        if ret then
            print("login success")
            local chat_msgs = skynet.call("database", "lua", "get_chat")
            skynet.send("watchdog", "lua", "dispatch_clients", "chat_msgs", {msgs = chat_msgs})
        end
        return ret
	else
		return {result = false}
	end
end

skynet.start(function()
	skynet.dispatch("lua", function(_,_, command, ...)
		local f = CMD[command]
		skynet.ret(skynet.pack(f(...)))
	end)
end)
