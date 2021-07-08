export interface IDiscountResponse {
    code: number,
    message?: string,
    Data: IDiscountInformation,
}

export interface IDiscountInformation {
    applicableDiscount: number
}