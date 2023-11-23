﻿using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Interfaces
{
    public interface IRabbitMensagemService
    {
        void EnviarMensagem(RabbitMensagemDTO mensagem);
    }
}
