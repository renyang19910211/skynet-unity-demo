local sprotoparser = require "sprotoparser"

local proto = {}

proto.c2s = sprotoparser.parse [[
.package {
	type 0 : integer
	session 1 : integer
}

handshake 1 {
	response {
		msg 0  : string
	}
}

get 2 {
	request {
		what 0 : string
	}
	response {
		result 0 : string
	}
}

set 3 {
	request {
		what 0 : string
		value 1 : string
	}
}

quit 4 {}

register_account 5 {
	request {
		account 0 : string
		password 1 : string
	}
	response {
		msg 0 : string
	}
}

heartbeat 6 {}

test_msg 7 {
	request {
		msg 0 : string
	}
}

]]



proto.s2c = sprotoparser.parse [[
.package {
	type 0 : integer
	session 1 : integer
}

heartbeat 1 {}

test_msg 2 {
	request {
		msg 0 : string
	}
}
]]

return proto
