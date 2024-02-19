import logoSrc from "@/assets/images/logo.png";

export default function Logo({ styles }: { styles?: string }) {
  return (
    <div>
      <img src={logoSrc} alt="FCAI logo" className={styles} />
    </div>
  );
}
