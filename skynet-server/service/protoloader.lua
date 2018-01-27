-- module proto as examples/proto.lua
package.path = "service/?.lua;" .. package.path

local skynet = require "skynet"
local sprotoparser = require "sprotoparser"
local sprotoloader = require "sprotoloader"
local proto = require "proto"

local PROTOS_FILE = "../protos/TestProto.sproto"

skynet.start(function()
	local handle = io.open(PROTOS_FILE, "r")
	local ret = handle:read("*a")
	handle:close()

	sprotoloader.save(sprotoparser.parse(ret), 1)
	-- don't call skynet.exit() , because sproto.core may unload and the global slot become invalid
end)
