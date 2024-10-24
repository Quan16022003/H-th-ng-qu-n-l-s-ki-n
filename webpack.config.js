const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CleanPlugin = require('clean-webpack-plugin');

module.exports = {
    watch: true,
    watchOptions: {
        poll: true,
        ignored: /node_modules/
    },

    entry: [
        './EventManagementSystem/wwwroot/js/site.js',
        './EventManagementSystem/wwwroot/scss/site.scss',
    ],
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, 'EventManagementSystem/wwwroot/dist'),
        publicPath: "/dist/"
    },
    module: {
        rules: [
            {
                test: /\.css$/, 
                use: ['style-loader', 'css-loader']
            },
            {
                test: /\.scss$/,
                use: ['style-loader', 'css-loader', 'sass-loader']
            }
        ]
    },

    devtool: "source-map",
    devServer: {
        open: true,
        static: path.join(__dirname, 'EventManagementSystem/wwwroot/dist'),
        port: 8080,
        hot: true,
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: './EventManagementSystem/Views/Shared/_Layout.cshtml',
            filename: "./site.html",
            cache: false
        }),
        new CleanPlugin.CleanWebpackPlugin({
            cleanOnceBeforeBuildPatterns: ['dist'],
        })
    ]
}