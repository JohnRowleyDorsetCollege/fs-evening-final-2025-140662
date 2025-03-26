select * from aspnetusers;
select * from DoctorPatient;

select patient.Username, doctor.Username from DoctorPatient as dp 
join aspnetusers as patient on dp.PatientId = patient.Id
join aspnetusers as doctor on dp.DoctorId = doctor.Id;
