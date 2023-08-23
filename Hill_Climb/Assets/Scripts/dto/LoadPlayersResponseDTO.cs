using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using dto;
using UnityEngine;

public class LoadPlayersResponseDTO : DTO
{
    public List<PlayerDTO> playerDTO { get; set; }
}
