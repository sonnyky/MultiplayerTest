# A test project for Unity Networking
This project is a test project to work with Unity's NetworkTransport API.

## Background
For a project, I needed a way to get multiple computers to talk to each other, and not in a server-client way, but in a p2p way where all clients can see each other and talk to each other.

## How to use this project
1. This Unity project was built with Unity 5.6.2, so it's not tested on older versions.
2. Clone or download the project to two different PCs with Unity 5.6.2 installed.
3. Make sure the PCs are in a local network (ex. connected to a common Wifi access point) can ping each other.
4. On the NetworkManager GameObject, find the NetworkShare script and setup the remote PCs IP address and ports. You need to input the address and port of the PC __you want to connect to__.
5. Inside the NetworkShare script, change the socketPort variable to a port of your choice (if necessary).
6. Hit Play.
7 If everything works well, you can press the S key and see the incoming message received on the other PC's Unity console.

## Credits
I followed the tutorial listed [here](http://www.robotmonkeybrain.com/good-enough-guide-to-unitys-unet-transport-layer-llapi/).
