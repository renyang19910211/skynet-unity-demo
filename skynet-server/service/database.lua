local mongo = require "skynet.db.mongo"
local skynet = require "skynet"
local db_account = require "db_account"

require "skynet.manager"	-- import skynet.register

local config = {
	host = "127.0.0.1",
    port = 27017,
}

local DB_NAME = "test_db"

local mongo_client_pool = {} --mongo对象池
local CMD = {}

local function create_db(key)
    local mongo_client = mongo.client(config)
    mongo_client_pool[key] = mongo_client
end

function CMD.get_db_account(account, password)
	print("DB_NAME ", DB_NAME)
	local collection = CMD.get_collection(DB_NAME , account)
	local ret
	if collection then
		ret = collection:findOne({_id = account})
	end
	skynet.ret(skynet.pack(ret))
end

function CMD.create(account, password)
    skynet.error("create db " .. password)
    create_db(DB_NAME)
    local collection = CMD.get_collection(DB_NAME, account)
	local ret = collection:findOne({_id = account})
	local result
	if ret then
		result = false
	else
		print("create success ", account)
		local t = {
	        account = account,
	        password = password,
	    }
		collection:update({_id = account}, t, true)
		result = true
	end
	skynet.ret(skynet.pack(result))
end

function CMD.get_db(key)
	local db = mongo_client_pool[key]
	if not db then create_db(DB_NAME) end
    return mongo_client_pool[key]
end

function CMD.get_collection(key, name)
    local db = CMD.get_db(key)
	if not db then return end
	return db[DB_NAME]:getCollection(name)
end

skynet.start (function ()
    skynet.dispatch("lua", function (session, source, cmd, ...)
        local f = assert(CMD[cmd])
        f(...)
    end)

    skynet.register("database")
end)
