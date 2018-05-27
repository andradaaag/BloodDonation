using BloodDonation.Data.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Data.Mapper
{
    public class FirebaseToObject
    {
        public Donor Donor(FirebaseObject<Donor> donor)
        {
            Donor d = donor.Object;
            d.ID = donor.Key;
            return d;
        }

        public DonationCenterPersonnel DonationCenterPersonnel(FirebaseObject<DonationCenterPersonnel> dcp)
        {
            DonationCenterPersonnel doncenp = dcp.Object;
            doncenp.ID = dcp.Key;
            return doncenp;
        }

        public Doctor Doctor(FirebaseObject<Doctor> doctor)
        {
            Doctor d = doctor.Object;
            d.ID = doctor.Key;
            return d;
        }

        public Donation Donation(FirebaseObject<Donation> donation)
        {
            Donation d = donation.Object;
            d.ID = donation.Key;
            return d;
        }

        public StoredBlood StoredBlood(FirebaseObject<StoredBlood> stored)
        {
            StoredBlood b = stored.Object;
            b.ID = stored.Key;
            return b;
        }
        public UserFirebase UserFirebase(FirebaseObject<UserFirebase> user)
        {
            UserFirebase u = user.Object;
            u.ID = user.Key;
            return u;
        }
        public Request Request(FirebaseObject<Request> request)
        {
            Request r = request.Object;
            r.ID = request.Key;
            return r;
        }

        public Hospital Hospital(FirebaseObject<Hospital> hospital)
        {
            Hospital h = hospital.Object;
            h.ID = hospital.Key;
            return h;
        }

        public Patient Patient(FirebaseObject<Patient> patient)
        {
            Patient p = patient.Object;
            p.ID = patient.Key;
            return p;
        }

        public DonationCenterPersonnel Personnel(FirebaseObject<DonationCenterPersonnel> personnel)
        {
            DonationCenterPersonnel p = personnel.Object;
            p.ID = personnel.Key;
            return p;
        }
    }
}
