import { useState } from "react";
import React from "react";
import { PriceEstimatorApi } from "../../services/priceEstimator/priceEstimator.api";


function PriceEstimator()
{
    const [pricePerGram, setPricePerGram] = useState(0.0);
    const [weight, setWeight] = useState(0.0);
    const [price, setPrice] = useState(0.0);
    const [isPrivilegedUser] = useState(false);
    const [finalPrice, setFinalPrice] = useState(0.0);
    const [discount, setDiscount] = useState(0);
    // Write a hook for this to fetch auth token from local storage.
    const authToken = "";

    React.useEffect(() => {
        // set discount value
        PriceEstimatorApi.getDiscount("gold", authToken);
    }, []);

    function handleSubmit(event: any) {
        event.preventDefault();

        if(isPrivilegedUser) {
            setFinalPrice(pricePerGram * weight);
            setFinalPrice(finalPrice);
        } else {
            setFinalPrice((pricePerGram * weight) - ((pricePerGram * weight * discount/100)));
        }
    }

    function downloadPdfInvoice()
    {
        PriceEstimatorApi.downloadPdf(pricePerGram, weight, discount, "customer-name", authToken);
    }

    function printPaperInvoice()
    {
        alert("This feature is under development");
    }

    return (
        <div className="col-lg-8 offset-lg-2">
            <h2>Login</h2>
            <form name="form" onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Price Per Gram</label>
                    <input type="text" name="pricePerGram" value={pricePerGram} />
                </div>
                <div className="form-group">
                    <label>Weight</label>
                    <input type="text" name="weight" value={weight} />
                </div>
                { isPrivilegedUser && <div className="form-group">
                    <label>Discount - {discount}</label>
                </div> }
                <div className="form-group">
                    <button className="btn btn-primary" onClick={handleSubmit}>
                        Calculate
                    </button>
                </div>
                <div className="form-group">
                    <button className="btn btn-primary" onClick={printPaperInvoice}>
                        To Printer
                    </button>
                </div>
                <div className="form-group">
                    <button className="btn btn-primary" onClick={downloadPdfInvoice}>
                        Download PDF
                    </button>
                </div>
            </form>
        </div>
    );
}

export { PriceEstimator };