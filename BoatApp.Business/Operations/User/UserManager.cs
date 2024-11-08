using SailTracker.Business.DataProtection;
using SailTracker.Business.Operations.User.Dtos;
using SailTracker.Business.Types;
using SailTracker.Data.Entities;
using SailTracker.Data.Enums;
using SailTracker.Data.Repositories;
using SailTracker.Data.UnitOfWork;

namespace SailTracker.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtection _protector;

        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository, IDataProtection protector)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _protector = protector;
        }

        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == user.Email.ToLower());
            if (hasMail.Any())
            {
                return new ServiceMessage
                {
                    IsSucced = false,
                    Message = "Email adresi zaten mevcut."
                };
            }

            var userEntity = new UserEntity()
            {
                Email = user.Email,
                FirstName = user.FirstName, // Yazım hatası düzeltildi
                LastName = user.LastName,
                Password = _protector.Cripted(user.Password), // Parolayı kriptola ve kaydet
                BirthDate = user.BirthDate,
                USer = UserType.Customer
            };

            _userRepository.Add(userEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Kullanıcı kaydında bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucced = true
            };
        }

        public ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user)
        {
            var userEntity = _userRepository.Get(x => x.Email.ToLower() == user.Email.ToLower());
            if (userEntity == null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucced = false,
                    Message = "Kullanıcı adı veya şifre hatalı"
                };
            }

            // Kullanıcının girdiği şifreyi veritabanındaki kriptolu şifre ile karşılaştırmak için decrypt işlemi yap
            var decryptedStoredPassword = _protector.UnCripted(userEntity.Password);
            if (decryptedStoredPassword == user.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucced = true,
                    Data = new UserInfoDto
                    {
                        Email = userEntity.Email,
                        Firstname = userEntity.FİrstName, // Yazım hatası düzeltildi
                        Lastname = userEntity.LastName,
                        UserType = userEntity.UserType
                    }
                };
            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucced = false,
                    Message = "Kullanıcı adı veya şifre hatalı"
                };
            }
        }
    }
}



