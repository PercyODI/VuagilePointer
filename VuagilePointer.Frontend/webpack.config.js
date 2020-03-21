///@ts-check

var path = require("path");
var webpack = require("webpack");
const HtmlWebpackPlugin = require("html-webpack-plugin");

/**
 * @param {string} env
 */
function getApiHost(env) {
    if (env == undefined || env == null){
        throw new Error("env undefined. Cannot continue.")
    }
    if (env.startsWith("dev"))
        return `'https://localhost:44397'`
    if (env.startsWith("prod"))
        return `'https://vuagile.centralus.cloudapp.azure.com'`
    throw new Error("env not recognized. env=" + env + ".")
}


module.exports = {
    module: {
        rules: [
            {
                test: /\.css$/,
                use: [
                    'style-loader',
                    'css-loader'
                ]
            },
            {
                test: /\.(woff|woff2|eot|ttf|otf|svg)$/,
                use: [
                    'file-loader',
                ],
            },
            { test: /\.hbs$/, loader: "handlebars-loader" }
        ],
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: "./src/index.html"
        }),
        new webpack.DefinePlugin({
            "__hostUrl__": getApiHost(process.env.NODE_ENV)
        })
    ],
    optimization: {
        splitChunks: {
            chunks: 'all',
        }
    },
    devtool: "inline-source-map",
    devServer: {
        contentBase: path.join(__dirname, "dist"),
        port: 9123
    }
}