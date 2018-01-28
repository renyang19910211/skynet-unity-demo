local skynet = require "skynet"
local route_handler = {}

-----------login event start--------
function route_handler.register_account(...)
    print("route_handler", "register_account")
    local login = skynet.newservice("login")
    return skynet.call(login, "lua", "register_account", ...)
end

function route_handler.login_account(...)
    print("route_handler", "login_account")
    local login = skynet.newservice("login")
    return skynet.call(login, "lua", "login_account", ...)
end
-----------login event end--------

-----------chat event start--------
function route_handler.chat_msg(...)
    local chat = skynet.newservice("chat")
    skynet.send(chat, "lua", "chat_msg", ...)
end

-----------chat event end--------

return route_handler
