type SingleError = {
  description: string;
};
type GlobalError = {
  type: string;
  title: string;
  errors: SingleError[];
};
