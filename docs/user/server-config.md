Server Documentation
====================
  
Installing
----------
  
1. Extract KMPServer into a folder. If you're reading this you probably got this far already.  
  
2. Copy these three files into the KMP Server folder:  
  * Assembly-CSharp.dll  
  * Assembly-CSharp-firstpass.dll  
  * UnityEngine.dll  
  
3. You're done!. Run KMPServer.exe (mono KMPServer.exe on mac/linux) and type /start to run the server with the default config.  
  
Command line configuration
--------------------------
  
These changes must be done before starting the server. If you have autoHost set, type /stop.  
Server config options are in camelcase, and case sensitive. For example you must type autoHost, not autohost or AUTOHOST.  
  
### ipBinding ###  
Default: /set ipBinding 0.0.0.0  
  
This can restrict what address KMP server will listen on.  
  
When to change: When you intend to run two different KMP servers on two different IP's with the same port number.  
  
### port ###  
Default: /set port 2076  
  
This will change what port number the KMP server will listen on.  
  
When to change: When running two servers, the second server will need a different unique number.  
  
### httpPort ###  
Default: /set httpPort 8081  
  
This will change what port number the HTTP status server will listen on.  
It can be found at http://localhost:8081/  
  
When to change: When running two servers, the second server will need a different unique number.  
  
### httpBroadcast ###  
Default: /set httpBroadcast true  
  
This will enable/disable the HTTP status server.  
  
When to change: When you do not want your HTTP status server to be accessible.  
  
### maxClients ###  
Default: /set maxClients 8  
  
How many clients the server will allow to be connected. Increasing this will result in more network and CPU usage.  
  
When to change: If you want to let more clients connect to your KMP server.  
  
### screenshotInterval ###  
Default: /set screenshotInterval 3000  
  
How often in milliseconds the client can upload a screenshot. 3000 milliseconds is 3 seconds.  
  
When to change: If uploading a screenshot once every 3 seconds is too slow, feel free to reduce this number.  
  
### autoRestart ###  
Default: /set autoRestart false  
  
Restarts the server automatically in case of a server crash.  
  
When to change: If you are running an unattended server, setting this to true is a very good idea.  
  
### autoHost ###  
Default: /set autoHost false  
  
Starts the server when you open KMPServer.exe, instead of having to type /start.  
  
When to change: After you are happy with the configuration options, set this to true for less hassle.  
  
### saveScreenshots ###  
Default: /set saveScreenshots true  
  
Saves all player screenshots in the screenshots/ directory.  
  
When to change: If you don't want to view or host player screenshots on a webserver.  
  
### hostIPv6 ###  
Default: /set hostIPv6 false  
  
When ipBinding is set to 0.0.0.0 (Listen on all IPv4 addresses), this setting changes it to :: (Which listens on all IPv4 and IPV6 addresses). Setting ipBinding to :: has the same effect.  
  
When to change: If you have working IPv6 connectivity and would like to support IPv6.  
  
### useMySQL ###  
Default: /set useMySQL false  
  
Uses a MySQL database if set to true. Make sure you set mySQLConnString when changing this setting.  
  
When to change: If you have a MySQL server and would prefer using it instead of sqlite (KMP_universe.db)  
  
### mySQLConnString ###  
Default: /set mySQLConnString   
  
Specifies the MySQL database to connect to. A connection string looks like this: Server=localhost;Database=KMPDatabase;Uid=KMPUsername;Pwd=KMPPassword;  
  
### backupInterval ###  
Default /set backupInterval 5  
  
Specifies how often the server will backup the database (Sqlite only).  
  
When to change: When you want the server to backup more or less often.  
  
### maxDirtyBackups ###  
Default: /set maxDirtyBackups  
  
How many times the server will back up the database before optimizing and cleaning the database.  
  
When to change: When you want the server to clean the database more or less often.  
  
### updatesPerSecond ###  
Default: /set updatesPerSecond 60  
  
The maximum amount of updates all clients combined can send per second.  
  
When to change: You really really shouldn't.  
  
### totalInactiveShips ###  
Default: /set totalInactiveShips 100  
  
How many inactive ships the server will send per update.  
  
When to change: You probably shouldn't.  
  
### consoleScale ###  
Default: /set consoleScale 1  
  
Changes how big the console text is. Windows only.  
  
When to change: When you cannot comfortably read the console text.  
  
### LogLevel ###  
Default: /set logLevel info  
  
Changes how much data appears in the log.  
  
When to change: When you are debugging KMP, or debugging on behalf of a developer.  
  
### maximumLogs ###  
Default: /set maximumLogs 100  
  
The amount of logs to keep in the logs/ directory.  
  
When to change: When you want to keep more or less logs.  
  
### ScreenshotHeight ###  
Default: /set screenshotHeight 600  
  
Resizes the screenshot before upload to the server. Set to 1080 for Full-HD screenshots. Changing this value will affect how much bandwidth a screenshot takes.  
  
When to change: When you intend to host Full-HD screenshot images or just intend to view them yourself.  
  
### autoDekessler ###  
Default: /set autoDekessler false  
  
Deletes all debris older than 30 minutes automatically  
  
When to change: Recommended to set to true for public servers.  
  
### autoDekesslerTime ###  
Default: /set autoDekesslerTime 30  
  
How often to run autoDekessler when set to on, in minutes.  
  
When to change: When you want to run autoDekessler more or less often.  
  
### profanityFilter ###  
Default: /set profanityFilter true  
  
Turns on or off the profanity filter.  
  
When to change: If you want to host a gucking swearing server.  
  
### profanityWords ###  
Default: fucker:kerper,faggot:kerpot,shit:kerp,fuck:guck,cunt:kump,piss:heph,fag:olp,dick:derp,cock:beet,asshole:hepderm,nigger:haggar  
  
When the profanity filter is on, changes words on the left side of the colon (:) to the right side of the colon (:).  
  
This is by far the most offensive sever setting you will ever see.    
You can have fun with this setting.  
  
When to change: When english changes its swear words (zooterkins), or when you decide space:spaaace would be funny.  
  
### whitelisted ###  
Default: /set whitelisted false  
  
Changes the server to whitelist only mode.  
  
Use /whitelist [add|del] [user] to update the whitelist.  
  
When to change: When you don't want random people joining your server.  
  
### joinMessage ###  
Default: /set joinMessage   
  
Changes the message displayed on server join.  
  
### serverInfo ###  
Default: /set serverInfo   
  
Changes the server info message displayed in the HTTP status message.  
  
### serverMotd ###  
Default: /set serverMotd   
  
Changes the message of the day, Very similar to joinMessage. Displayed with !motd  
  
### serverRules ###  
Default: /set serverRules  
  
Changes the rules message. Displayed with !rules  
  
### safetyBubbleRadius ###  
Default: /set safetyBubbleRadius 2000  
  
Changes the size of the safety "cylinder", in meters across. The safety "cylinder" is hard coded to be 35km high.  
  
1700 just protects the end of the runway, you most likely don't want to set it lower than this.  
  
When to change: When you want the safety bubble to be a different size.  
  
### cheatsEnabled ###  
Default: /set cheatsEnabled true  
  
Enables or disables the in-game F12 cheats.  
  
When to change: When you don't want players cheating.  
  
### allowPiracy ###  
Default: /set allowPiracy false  
  
Enables wether or not private docking ports are enabled on other players ships. The target still unsets, so you have to do the last 500 meters without the navball. This is tricky.  
  
When to change: When you want (very skilled) players to be able to take over private ships by docking to them.  
  
### Game Mode (gameMode) ###  
Default: /mode sandbox  
  
Changes the game mode from sandbox or career.  
  
When to change: When you want your players to do !!!SCIENCE!!!.  
  
## TL;DR ##  
Sane changes are this:  
/set autoHost true  
/set autoRestart true  
/set autoDekessler true  
/set safetyBubbleRadius 1700  
  
Mods  
----  
  
Introduced in 0.1.5, the server can now actually control the mods in order to connect.  
  
The file you want to edit will be called KMPModControl.txt, It appears on the first /start.  
  
## !required ##  
A list of folder names needed in GameData in order for the client to connect. Folders usually imply the mods are installed under them.  
  
You should put every part-adding mod in the required section. Or Bad Things(TM) may happen to either your family or your cat (or other beloved pets).  
  
Example:  
!required  
MechJeb2  
Kethane  
  
## !md5 ##  
Needed if you want to enforce a specific version. Usually you can leave this section blank.  
  
Example:  
!md5  
MechJeb2/Plugins/MechJeb2.dll=64E6E05C88F3466C63EDACD5CF8E5919  
  
## !resource-blacklist or !resource-whitelist ##  
You must chose either or. Blacklisting will ban specific mods and allow all others, whitelisting will allow specific mods and ban all others.  
  
The default is to enable all mods. Optional mods like Kerbal Alarm Clock would go in the whitelist if you are running whitelisting.  
Whitelisting is safer in terms of cheating, blacklisting is easier for clients to join with their favourite mods.  
  
Mechjeb 2 blacklisting (Don't do this, parts are already banned):  
Example  
!resource-blacklist  
MechJeb2.dll  
  
OR (Do this if you are running a whitelist server, with mechjeb also being in the required section)  
  
!resource-whitelist  
MechJeb2.dll  
  
## !partslist ##  
  
The list of parts enabled in the game. You must change underscores "_" to periods "."  
A handy (and recommended) application for this, made by "CreationMe/maxvandelaar", can be found here: http://forum.kerbalspaceprogram.com/threads/57284  
