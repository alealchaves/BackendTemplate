using Moq;
using AutoFixture;
using BackendTemplate.Domain.Interfaces.UsuarioInterfaces;
using BackendTemplate.Domain.Core.DTO;
using BackendTemplate.Domain.DTO.UsuarioDTOs;
using BackendTemplate.Domain.Entities;
using BackendTemplate.Domain.Services.Usuario;
using BackendTemplate.Domain.Core.i18n;

namespace Test.Services
{
    public class UsuarioSelectServiceTest
    {
        private readonly Mock<IUsuarioRepository> _usuarioMockRepository;
        private readonly Mock<IUsuarioLoginRequestValidator> _usuarioLoginMockRequestValidator;
        private readonly Mock<IGlobalizationResource> _localizerMock;
        private readonly UsuarioSelectService _usuarioSelectService;
        Fixture fixture;

        public UsuarioSelectServiceTest()
        {
            _usuarioMockRepository = new Mock<IUsuarioRepository>();
            _usuarioLoginMockRequestValidator = new Mock<IUsuarioLoginRequestValidator>();
            _localizerMock = new Mock<IGlobalizationResource>();

            _usuarioSelectService = new UsuarioSelectService(
                _usuarioMockRepository.Object,
                _usuarioLoginMockRequestValidator.Object,
                _localizerMock.Object);
        }

        [Fact]
        public void Select()
        {
            fixture = CriarFixtureSemRecursao();
            
            var retornoMockUsuario = fixture.Build<UsuarioResponse>().Create();

            _usuarioMockRepository.Setup(x => x.Select<UsuarioResponse>())
                .ReturnsAsync(new List<UsuarioResponse>() { retornoMockUsuario });
            var resultado = _usuarioSelectService.Select();

            Assert.NotNull(resultado);
        }

        private Fixture CriarFixtureSemRecursao()
        {
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return fixture;
        }
    }
}