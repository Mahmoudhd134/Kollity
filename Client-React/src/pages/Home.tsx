import Logo from "@/components/ui/Logo";
import { Button } from "@/components/ui/button";
import { useEffect } from "react";
import { signal } from "@preact/signals";
import { ChevronsDown } from "lucide-react";

import img1 from "@/assets/images/university/fcai_001.jpg";
import img2 from "@/assets/images/university/fcai_002.jpg";
import img3 from "@/assets/images/university/fcai_003.jpg";
import img4 from "@/assets/images/university/fcai_004.jpg";
import img5 from "@/assets/images/university/fcai_005.jpg";
import img6 from "@/assets/images/university/fcai_006.jpg";
import { Link } from "react-router-dom";

const images = [img1, img2, img3, img4, img5, img6];
const currentSlide = signal(0);
export default function Home() {
  useEffect(() => {
    const changeSliderTimer = setInterval(() => {
      const images = document.querySelectorAll(".slider img");

      images.forEach((img, index) => {
        if (currentSlide.value === index) {
          img.classList.remove("opacity-100");

          if (currentSlide.value === images.length - 1) {
            images[0].classList.add("opacity-100");
          } else {
            images[index + 1].classList.add("opacity-100");
          }
        }
      });

      currentSlide.value =
        currentSlide.value + 1 === images.length ? 0 : currentSlide.value + 1;
    }, 5000);

    return () => {
      clearInterval(changeSliderTimer);
    };
  }, []);

  return (
    <section
      className="flex flex-col items-center justify-center gap-4 h-screen relative"
      id="helloSection"
    >
      {/* Slider */}
      <div className="slider absolute w-full h-full z-[-1] bg-slate-500">
        {images.map((img, index) => (
          <img
            key={index}
            src={img}
            alt="FCAI"
            className={`w-full h-full object-cover absolute top-0 left-0 transition-opacity duration-700 opacity-0 ${
              currentSlide.value === index && "opacity-100"
            }`}
            style={{
              filter: "brightness(0.5)",
            }}
          />
        ))}
      </div>

      <div className="absolute left-1/2 -translate-x-1/2 text-white bottom-20">
        <ChevronsDown size={32} />
      </div>

      <Logo styles="w-[7.8em]" />

      <div className="text-center text-white">
        <h1>
          أهلا بك في منصة
          <br />
          كلية حاسبات ومعلومات
        </h1>
      </div>

      <Link to={"/sign-in"}>
        <Button>تسجيل الدخول</Button>
      </Link>
    </section>
  );
}
