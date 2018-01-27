local skynet = require "skynet"

skynet.start(function()
	skynet.uniqueservice("protoloader")
	skynet.newservice("database")

  --打开监察服务
	local watchdog = skynet.newservice("watchdog")
	skynet.call(watchdog, "lua", "start", {
		port = 8899, --监听端口
		maxclient = max_client, -- 最大连接数量
		nodelay = true,
	})
	skynet.exit()
end)
