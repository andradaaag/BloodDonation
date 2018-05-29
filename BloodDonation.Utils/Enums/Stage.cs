
namespace BloodDonation.Utils.Enums
{
/**
 * 
 */
	public enum Stage
    {
        Sampling,
        //Each donor must complete a form of his/her lifestyle and medical history. 
        //They will next go to a doctor who will decide the prospective donor’s aptitude 
        //for donation. If they are deemed qualified for donation a certified medical 
        //staff will harvest the blood.
        Preparation,
        //Each harvested container will go through a process of filtration, centrifuge, 
        //and separation to obtain the components of blood: red blood cells, plasma, 
        //and thrombocytes. If a patient need only one component then they will receive 
        //only that part.
        BiologicalQualityControl,
        //Each sample is tested on two fronts: Immuno-Hematology and blood transmissible 
        //diseases (HIV / AIDS, hepatitis B, hepatitis C, syphilis, HTLV, ALT). If any 
        //sample fails a test it is marked unusable and the donor is privately notified.
        Redistribution,
        //The containers are redistributed among the hospitals and clinics that need them 
        //with each part having the following shelf life:
        //•	Thrombocytes – 5 days
        //•	Red blood cells – 42 days
        //•	Plasma – several months
        Failed
        //Failed any of the tests
            

    }
}