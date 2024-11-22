const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const CleanPlugin = require("clean-webpack-plugin");
const webpack = require("webpack");

module.exports = {
  watch: true,
  watchOptions: {
    poll: true,
    ignored: /node_modules/,
  },

  mode: "development",

  entry: [
    "./EventManagementSystem/wwwroot/js/site.js",
    "./EventManagementSystem/wwwroot/scss/site.scss",
  ],
  output: {
    filename: "bundle.js",
    path: path.resolve(__dirname, "EventManagementSystem/wwwroot/dist"),
    publicPath: "/dist/",
  },
  module: {
    rules: [
      {
        test: require.resolve("jquery"),
        use: [
          {
            loader: "expose-loader",
            options: {
              exposes: ["$", "jQuery"],
            },
          },
        ],
      },
      {
        test: /\.css$/,
        use: ["style-loader", "css-loader"],
      },
      {
        test: /\.scss$/,
        use: ["style-loader", "css-loader", "sass-loader"],
      },
      {
        test: /datatables\.net.*/,
        use: [
          {
            loader: "imports-loader",
            options: {
              additionalCode: 'var define = false;'
            },
          },
        ],
      },
    ],
  },

  devtool: "source-map",
  devServer: {
    open: true,
    static: path.join(__dirname, "EventManagementSystem/wwwroot/dist"),
    port: 8080,
    hot: true,
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: "./EventManagementSystem/Views/Shared/_Layout.cshtml",
      filename: "./site.html",
      cache: false,
    }),
    new CleanPlugin.CleanWebpackPlugin({
      cleanOnceBeforeBuildPatterns: ["dist"],
    }),
    new webpack.ProvidePlugin({
      $: "jquery",
      jQuery: "jquery",
    }),
  ],
};
