skynet_root = "lib/skynet/" --skynet根目录
root = "./" --当前项目根目录

-- preload = "./examples/preload.lua"	-- run preload.lua before every lua service run
thread = 2
logger = nil
harbor = 1
address = "127.0.0.1:2579"--当前 skynet 节点的地址和端口，方便其它节点和它组网。注：即使你只使用一个节点，也需要开启控制中心，并额外配置这个节点的地址和端口。
master = "127.0.0.1:2048"--指定 skynet 控制中心的地址和端口，如果你配置了 standalone 项，那么这一项通常和 standalone 相同。
start = "main"	-- 这是 bootstrap 最后一个环节将启动的 lua 服务，也就是你定制的 skynet 节点的主程序。默认为 main ，即启动 main.lua 这个脚本。这个 lua 服务的路径由下面的 luaservice 指定。
bootstrap = "snlua bootstrap"	--启动的第一个服务以及其启动参数。默认配置为 snlua bootstrap ，即启动一个名为 bootstrap 的 lua 服务。通常指的是 service/bootstrap.lua 这段代码。
standalone = "0.0.0.0:2048"--如果把这个 skynet 进程作为主进程启动（skynet 可以由分布在多台机器上的多个进程构成网络），那么需要配置standalone 这一项，表示这个进程是主节点，它需要开启一个控制中心，监听一个端口，让其它节点接入。

lua_cpath = skynet_root.."luaclib/?.so;"--将添加到 package.cpath 中的路径，供 require 调用。
lua_path = skynet_root.."lualib/?.lua;"..skynet_root.."lualib/?/init.lua;"..root.."lua/?.lua"--将添加到 package.path 中的路径，供 require 调用。
luaservice = skynet_root .. "service/?.lua;" .. root .. "service/?.lua"--lua服务路径

lualoader = skynet_root.."lualib/loader.lua" -- 用哪一段 lua 代码加载 lua 服务。通常配置为 lualib/loader.lua ，再由这段代码解析服务名称，进一步加载 lua 代码。snlua 会将下面几个配置项取出，放在初始化好的 lua 虚拟机的全局变量中。具体可参考实现。

--snax = skynet_root.."?.lua;" .. root .. "service?.lua;" --snax模版路径
-- snax_interface_g = "snax_g"
cpath = skynet_root.."cservice/?.so" --用 C 编写的服务模块的位置，通常指 cservice 下那些 .so 文件。如果你的系统的动态库不是以 .so 为后缀，需要做相应的修改。这个路径可以配置多项，以 ; 分割。
-- daemon = "./skynet.pid"
