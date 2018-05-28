namespace BloodDonation.Logic.Models
{
    public class BloodType
    {
        public string Group;
        public bool RH;

        public bool isCompatible(BloodType destination)
        {

            string sourceT = this.Group;
            if (this.RH)
                sourceT += "+";
            else
                sourceT += "-";

            string destT = destination.Group;
            if (destination.RH)
                destT += "+";
            else
                destT += "-";


            switch (sourceT)
            {
                case "O-":
                    return true;

                case "O+":
                    if (destT == "O-" || destT == "A-" || destT == "B-" || destT == "AB-")
                        return false;
                    return true;

                case "A-":
                    if (destT == "O-" || destT == "O+" || destT == "B-" || destT == "B+")
                        return false;
                    return true;

                case "A+":
                    if (destT == "A+" || destT == "AB+")
                        return true;
                    return false;

                case "B-":
                    if (destT == "O-" || destT == "O+" || destT == "A-" || destT == "A+")
                        return false;
                    return true;

                case "B+":
                    if (destT == "B+" || destT == "AB+")
                        return true;
                    return false;

                case "AB-":
                    if (destT == "AB-" || destT == "AB+")
                        return true;
                    return false;

                case "AB+":
                    return destT == "AB+";

                default:
                    return false;


            }
        }
    }
}