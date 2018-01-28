local skynet = require "skynet"
local CMD = {}

function CMD.chat_msg(args)
    print("on chat msg", args.msg)
    skynet.send("watchdog", "lua", "dispatch_clients", "chat_msg", {msg = args.msg})
    skynet.send("database", "lua", "save_chat", args.msg)
end

skynet.start(function()
	skynet.dispatch("lua", function(_,_, command, ...)
		local f = CMD[command]
		skynet.ret(skynet.pack(f(...)))
	end)
end)
