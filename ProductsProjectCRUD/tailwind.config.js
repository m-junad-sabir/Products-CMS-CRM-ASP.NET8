module.exports = {
    content: [
        "./Pages/**/*.cshtml",
        "./Views/**/*.cshtml",
        "./Shared/**/*.cshtml",
        "./wwwroot/**/*.js",
        "./node_modules/flowbite/**/*.js"
    ],
    theme: {
        extend: {},
    },
    plugins: [
        require("daisyui"),
        require("flowbite/plugin")
    ],
    daisyui: {
        themes: ["light", "dark", "corporate", "emerald"],
    }
}
