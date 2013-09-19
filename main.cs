// Display a splash window immediately to improve app responsiveness before
// engine is initialized and main window created
displaySplashWindow("splash.bmp");

// Console does something.
setLogMode(2);
// Disable script trace.
trace(false);

//-----------------------------------------------------------------------------
// Load up scripts to initialise subsystems.
exec("sys/main.cs");

// The canvas needs to be initialized before any gui scripts are run since
// some of the controls assume that the canvas exists at load time.
createCanvas("T3Dbones");

// Start rendering and stuff.
initRenderManager();
initLightingSystems("Basic Lighting"); 

// Start PostFX. If you use "Advanced Lighting" above, uncomment this.
//initPostEffects();

// Start audio.
sfxStartup();

//-----------------------------------------------------------------------------
// Load console.
exec("console/main.cs");

// Load up game code.
exec("tutorials/05_aiplayer_intro/main.cs");

// Called when we connect to the local game.
function GameConnection::onConnect(%client) {
   %client.transmitDataBlocks(0);
}

// Called when all datablocks from above have been transmitted.
function GameConnection::onDataBlocksDone(%client) {
   // Start sending ghosts to the client.
   %client.activateGhosting();
   %client.onEnterGame();
}

// Create a local game server and connect to it.
new SimGroup(ServerGroup);
new GameConnection(ServerConnection);
// This calls GameConnection::onConnect.
ServerConnection.connectLocal();

onStart();
