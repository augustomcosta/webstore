module.exports = {
  content: ["'./src/**/*.{html,js,ts}"],
  theme: {
    extend: {
      fontFamily: ["Montserrat", "sans-serif"],
      backgroundImage: {
        "login-bg": "url('/assets/images/login.jpg')",
      },
      colors: {
        gradientColor: {
          blue: "#0672B9",
          cyan: "#6ADBDB",
          lime: "#DCFFCC",
          emerald: "#06B96E",
          orange: "#FFC85C",
          tangerine: "#DB6A6A",
          pink: "#D55CFF",
          purple: "#736ADB",
          jade: "#186F5A",
          turquoise: "#72E9B0",
          sky: "#8EDCF3",
          nightSky: "#8691EF",
          darkTeal: "#006B8D",
          augusteal: "#13beb2",
          darkAugusteal: "#0a4b46",
        },
      },
    },
  },
  plugins: [],
};
