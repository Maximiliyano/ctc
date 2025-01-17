﻿using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Abstractions;
using ChatTeamChallenge.Domain.Core.Events.Chat;
using ChatTeamChallenge.Domain.Core.Primities;

namespace ChatTeamChallenge.Domain.Apartments;

public sealed class ChatMember : AggregateRoot, IAuditableEntity
{
    public required int UserId { get; set; }
    public required int ChatId { get; set; }
    
    public DateTime CreatedAt { get; private init; }
    public DateTime? UpdatedAt { get; private init; }
    
    public ChatMemberRoles Role { get; set; }
    
    public Chat? Chat { get; set; }
    public User? User { get; set; }

    public static ChatMember Create(int userId, int chatId, ChatMemberRoles role, int? id = null)
    {
        var chatMember = new ChatMember
        {
            Id = id ?? 0,
            UserId = userId,
            ChatId = chatId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            Role = role
        };
        
        chatMember.AddDomainEvent(new ChatMemberCreatedDomainEvent(chatMember));
        
        return chatMember;
    }
}