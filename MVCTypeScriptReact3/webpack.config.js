module.exports = {
    entry:
        {
            page1: "./Scripts/src/index.tsx",
            page2: "./Scripts/src/index2.tsx",
            commentBoxPage: "./Scripts/src/CommentBoxIndex.tsx"
        },
    output: {
        filename: "[name].bundle.js",
        path: __dirname + "/Scripts/dist/"
    },

    // Enable sourcemaps for debugging webpack's output.
    devtool: "source-map",

    resolve: {
        // Add '.ts' and '.tsx' as resolvable extensions.
        extensions: ["", ".webpack.js", ".web.js", ".ts", ".tsx", ".js"]
    },

    module: {
        loaders: [
            // All files with a '.ts' or '.tsx' extension will be handled by 'ts-loader'.
            { test: /\.tsx?$/, loader: "ts-loader" }
        ],

        preLoaders: [
            // All output '.js' files will have any sourcemaps re-processed by 'source-map-loader'.
            { test: /\.js$/, loader: "source-map-loader" }
        ]
    }
}