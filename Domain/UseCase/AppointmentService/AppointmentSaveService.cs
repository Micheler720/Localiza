using System;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.UseCase.AppointmentService.Exceptions;
using Domain.Shared.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using Domain.UseCase.AppointmentService.View;

namespace Domain.UseCase.AppointmentService
{
    public class AppointmentSaveService
    {
        private IAppointmentRepository<Appointment> _repository;
        private ICarRepository<Car> _repositoryCar;
        private IClientRepository<Client> _repositoryClient;
        private IOperatorRepository<Operator> _repositoryOperator;
        private IPDFWriter _servicePDF;

        public AppointmentSaveService(
            IAppointmentRepository<Appointment> repository, 
            ICarRepository<Car> repositoryCar,
            IClientRepository<Client> repositoryClient,
            IOperatorRepository<Operator> repositoryOperator,
            IPDFWriter servicePDF
            )
        {
            _repository = repository;
            _repositoryCar = repositoryCar;
            _repositoryClient = repositoryClient;
            _repositoryOperator = repositoryOperator;
            _servicePDF = servicePDF;
        }

        public async Task Execute(Appointment appointment, string path)
        {
            if(appointment.HourPrice <= 0 ) throw new ValuesInvalidException("Valor de hora por locação invalido. Verifique!");
            if(appointment.HourLocation <= 0 ) throw new ValuesInvalidException("Quantidade de horas locadas invalido. Verifique!");
            if(appointment.Subtotal <= 0 ) throw new ValuesInvalidException("SubTotal Inválido. Verifique!");
            if(appointment.Amount <= 0 ) throw new ValuesInvalidException("Total invalido.Verifique!");

            var car = await _repositoryCar.FindById(appointment.IdCar);
            if(car.Board == null) throw new NotFoundRegisterException("Carro não encontrado, verifique informações.");
                
            var client = await _repositoryClient.FindById(appointment.IdClient);
            if(client.Cpf == null) throw new NotFoundRegisterException("Cliente não encontrado, verifique informações.");
                
            var op = await _repositoryOperator.FindById(appointment.IdOperator);
            if(op.Registration == null) throw new NotFoundRegisterException("Operador não encontrado, verifique informações.");
            
            
            if(appointment.DateTimeExpectedCollected > appointment.DateTimeExpectedDelivery) throw new DateTimeColectedInvalidException("Data esperada da coleta maior que a data esperada para entrega. Verifique.");
            appointment.Car = car;
            appointment.Client = client;
            appointment.Amount = car.HourPrice * appointment.HourLocation;
            appointment.Subtotal = appointment.Amount;

            string pdf;

            if (appointment.Id == 0)
            {
                if(appointment.DateTimeExpectedCollected < DateTime.Now) throw new DateTimeColectedInvalidException("Data esperada da coleta menor que data atual. Verifique!");   

                var avalabilityCar = await _repository.CheckAvailabilityCar(appointment.IdCar, appointment.DateTimeExpectedCollected);
                if(!avalabilityCar ) throw new CarNotAvalabityException("Carro já está alocado.");
                
                var avalabilityClient = await _repository.CheckAvailabilityClient(appointment.IdClient, appointment.DateTimeExpectedCollected);
                if(!avalabilityClient) throw new ClientNotAvalabityException("Cliente já possui um agendamento para está data.");
               

                appointment.IdCheckList = null;
                appointment.DateTimeCollected = null;
                appointment.DateTimeDelivery = null;
                appointment.Schedule = DateTime.Now;
                
                await _repository.Add(appointment);
                pdf = LeaseAgreementToPDF.Writer(appointment);
                _servicePDF.Build(path, pdf);

                return;
            }


            await _repository.Update(appointment);
            pdf = LeaseAgreementToPDF.Writer(appointment);
            _servicePDF.Build(path, pdf);
        }
    }
}