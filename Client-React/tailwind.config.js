/** @type {import('tailwindcss').Config} */
import withMT from '@material-tailwind/react/utils/withMT.js'

const tailwindConfig = {
    content: [
        "./src/**/*.{js,ts,jsx,tsx,mdx}",
    ], theme: {
        extend: {},
    },
    plugins: [],
    darkMode:'selector'
}
export default withMT(tailwindConfig)

