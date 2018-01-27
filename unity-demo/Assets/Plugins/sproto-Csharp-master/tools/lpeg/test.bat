set LUA_DIR=E:\Cache\Documents\UnityProjects\sproto-demo\Assets\Plugins\lua-5.3.4
gcc -O2 -shared -s -I %LUA_DIR%\src -L %LUA_DIR%\src -o lpeg.dll lptree.c lpvm.c lpcap.c lpcode.c lpprint.c -llua53
pause
