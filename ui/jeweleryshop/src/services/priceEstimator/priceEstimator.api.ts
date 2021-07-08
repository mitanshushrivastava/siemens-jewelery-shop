import { DiscountEnquiryUrl, DownloadPdfUrl } from "../endpoints";
import { IDiscountResponse } from "../priceEstimator/priceEstimator.contracts";
import axios from 'axios';

// can weite custom hook to fetch authToken insted of passing everytime.

const downloadPdf = async function(pricePerperGram: number, weight: number, discount:number, name: string, authToken: string) {
    // set auththoken in authorization header.
    const response = await axios.post<any>(DownloadPdfUrl, {
        pricePerperGram: pricePerperGram,
        weight: weight,
        discount: discount,
        name: name
    });

    return response.data;
}

const getDiscount = async function(productName: string, authToken: string) {
    // set auththoken in authorization header.
    const response = await axios.post<IDiscountResponse>(DiscountEnquiryUrl, {
        productName: productName
    });

    return response.data.Data.applicableDiscount;
}

export const PriceEstimatorApi = {
    downloadPdf,
    getDiscount
};