const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;
const CopyPlugin = require('copy-webpack-plugin');
const bundleOutputDir = './wwwroot/dist';
const rootPath = './wwwroot';

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    return [{
        stats: { modules: false },
        entry: { 'main': './ClientApp/boot-app.js' },
        resolve: {
            extensions: ['.js', '.vue'],
            alias: {
                'vue$': 'vue/dist/vue',
                'components': path.resolve(__dirname, './ClientApp/components'),
                'views': path.resolve(__dirname, './ClientApp/views'),
                'utils': path.resolve(__dirname, './ClientApp/utils'),
                'api': path.resolve(__dirname, './ClientApp/store/api')
            }
        },
        output: {
            path: path.join(__dirname, bundleOutputDir),
            filename: '[name].js',
            publicPath: '/dist/'
        },
        node: {
            fs: 'empty'
        },
        module: {
            rules: [
                { test: /\.vue$/, include: /ClientApp/, use: 'vue-loader' },
                { test: /\.js$/, include: /ClientApp/, use: 'babel-loader' },
                { test: /\.ts$/, include: /ClientApp/, use: 'awesome-typescript-loader?silent=true' },
                { test: /\.css$/, use: isDevBuild ? ['style-loader', 'css-loader'] : ExtractTextPlugin.extract({ use: 'css-loader' }) },
                { test: /\.(png|jpg|jpeg|gif|svg)$/, use: 'url-loader?limit=25000' },
                {
                    test: /\.woff2?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
                    // Limiting the size of the woff fonts breaks font-awesome ONLY for the extract text plugin
                    // loader: "url?limit=10000"
                    use: "url-loader"
                },
                {
                    test: /\.(ttf|eot|svg)(\?[\s\S]+)?$/,
                    use: 'file-loader'
                },
                {
                    test: /font-awesome\.config\.js/,
                    use: [
                        { loader: "style-loader" },
                        { loader: "font-awesome-loader" }
                    ]
                }
            ]
        },
        plugins: [
            new CheckerPlugin(),
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/dist/vendor-manifest.json')
            }),
            new CopyPlugin([
                // Copy directory contents to {output}/to/directory/
                { from: path.join(__dirname, './admin'), to: path.join(__dirname, bundleOutputDir, './admin') },
                { from: path.join(__dirname, './blob'), to: path.join(__dirname, rootPath, '/blob') }
            ])
        ].concat(isDevBuild ? [
            // Plugins that apply in development builds only
            new webpack.SourceMapDevToolPlugin({
                filename: '[file].map', // Remove this line if you prefer inline source maps
                moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
            })
            ] : [
                // Plugins that apply in production builds only
                new webpack.optimize.UglifyJsPlugin(),
                //new ExtractTextPlugin('site.css')
                new ExtractTextPlugin({
                    filename: (getPath) => {
                        return getPath('css/[name].css');
                    },
                    allChunks: true
                })
            ])
    }];
};
