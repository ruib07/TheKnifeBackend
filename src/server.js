const config = require('./config');

const app = require('./app');

app.listen(config.NODE_PORT, () => {
  console.log(`APP LISTENING: ${config.NODE_PORT}`);
});
