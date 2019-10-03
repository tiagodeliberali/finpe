const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');

module.exports = (env) => {
  const processEnv = {};

  if (env.BRANCH === 'local') {
    processEnv.API_URL = 'https://localhost:44362/api/';
    processEnv.AUTH_AUDIENCE = 'https://finpe-api-forno.azurewebsites.net';
  } else if (env.BRANCH === 'forno') {
    processEnv.API_URL = 'https://finpe-api-forno.azurewebsites.net/api/';
    processEnv.AUTH_AUDIENCE = 'https://finpe-api-forno.azurewebsites.net';
  } else if (env.BRANCH === 'master') {
    processEnv.API_URL = 'https://finpe-api.azurewebsites.net/api/';
    processEnv.AUTH_AUDIENCE = 'https://finpe-api.azurewebsites.net';
  }

  processEnv.AUTH_DOMAIN = 'dev-ufffqdk9.auth0.com';
  processEnv.AUTH_CLIENT_ID = 'elsGCuiL2k3t83hWA5io6xbvlgtXMsX9';

  return {
    entry: ['@babel/polyfill', './src/index.js'],
    mode: 'development',
    module: {
      rules: [{
        test: /\.(js|jsx)$/,
        exclude: /(node_modules|bower_components)/,
        loader: 'babel-loader',
        options: {
          presets: ['@babel/env'],
        },
      },
      {
        test: /\.css$/,
        use: ['style-loader', 'css-loader'],
      },
      ],
    },
    resolve: {
      alias: {
        'react-dom': '@hot-loader/react-dom',
      },
      extensions: ['*', '.js', '.jsx'],
    },
    output: {
      path: path.resolve(__dirname, 'dist'),
      filename: '[name].[hash].js',
    },
    devServer: {
      contentBase: path.join(__dirname, 'public/'),
      port: 3000,
      publicPath: 'http://localhost:3000/dist/',
      hotOnly: true,
    },
    plugins: [
      new webpack.HotModuleReplacementPlugin(),
      new webpack.DefinePlugin({
        'process.env.API_BASE_URL': JSON.stringify(processEnv.API_URL),
        'process.env.AUTH_DOMAIN': JSON.stringify(processEnv.AUTH_DOMAIN),
        'process.env.AUTH_CLIENT_ID': JSON.stringify(processEnv.AUTH_CLIENT_ID),
        'process.env.AUTH_AUDIENCE': JSON.stringify(processEnv.AUTH_AUDIENCE),
      }),
      new CleanWebpackPlugin(),
      new HtmlWebpackPlugin({
        template: 'public/index.hbs',
      }),
    ],
  };
};
