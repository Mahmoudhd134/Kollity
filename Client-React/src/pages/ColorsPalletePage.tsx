export default function ColorsPalletePage() {
  const divClasses =
    "flex items-center justify-center text-white text-2xl font-bold ";

  return (
    <>
      <div className="grid grid-cols-4 gap-5">
        <div className={"w-48 h-48 bg-primary " + divClasses}>Primary</div>
        <div className={"w-48 h-48 bg-primary-100 " + divClasses}>100</div>
        <div className={"w-48 h-48 bg-primary-200 " + divClasses}>200</div>
        <div className={"w-48 h-48 bg-primary-300 " + divClasses}>300</div>
        <div className={"w-48 h-48 bg-primary-400 " + divClasses}>400</div>
        <div className={"w-48 h-48 bg-primary-500 " + divClasses}>500</div>
      </div>
    </>
  );
}
