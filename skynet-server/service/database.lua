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

function CMD.create(account, password)
    skynet.error("create db " .. password)
    create_db("abc")
    local collection = CMD.get_collection("abc", account)

    local t = {
        account = account,
        password = password,
    }
    collection:update({_id = account}, t, true)
    local ret = collection:findOne({_id = account})
    print(ret.account, ret.password)
end

function CMD.get_db(key)
    return mongo_client_pool[key]
end

function CMD.get_collection(key, name)
    local db = CMD.get_db(key)
	return db[DB_NAME]:getCollection(name)
end

skynet.start (function ()
    skynet.dispatch("lua", function (session, source, cmd, ...)
        local f = assert(CMD[cmd])
        f(...)
    end)

    skynet.register("database")
end)
