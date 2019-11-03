using System.Collections.Generic;
using testeEFCore.Business.Notificacoes;

namespace testeEFCore.Business.Intefaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
