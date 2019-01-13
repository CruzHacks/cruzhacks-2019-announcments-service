using System;
using Microsoft.EntityFrameworkCore;
using cruzhacks_2019_announcments_service.Models;

public class MessageContext : DbContext
{
	public MessageContext(DbContextOptions<MessageContext> options) : base (options) {}
    public DbSet<Message> StoredMessages { get; set; }
}
