const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    entry: './EventManagementSystem/wwwroot/js/site.js',
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, 'EventManagementSystem/wwwroot/dist')
    },
    module: {
        rules: [
            {
                test: /\.scss$/,
                use: ['style-loader', 'css-loader', 'sass-loader']
            }
        ]
    },
    devServer: {
        static: path.resolve(__dirname, 'dist'),
        port: 8080,
        hot: true
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: './EventManagementSystem/Views/Shared/_Layout.cshtml',
            filename: "./site.html"
        })
    ]
}