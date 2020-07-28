import { AbstractControl, ValidatorFn } from '@angular/forms';

export function ageValidator(age: number): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
        const birthDate = control.value as Date;
        if (birthDate instanceof Date) {
            const timeDiff = Math.abs(Date.now() - birthDate.getTime());
            const currentAge = Math.floor((timeDiff / (1000 * 3600 * 24)) / 365.25);

            return currentAge < age ? {ageValidator: true,  ageValue: age} : null;
        }

        return {ageValidator: true, ageValue: age};
    };
}
