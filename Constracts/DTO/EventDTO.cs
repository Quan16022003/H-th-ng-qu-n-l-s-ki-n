﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class EventDTO : BaseDTO
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsPublic { get; set; } = false;

        public string? Slug { get; set; }
        public string? OrganizerId { get; set; }

        [Required]
        public UserDTO? Organizer { get; set; }

        [Required]
        public EventCategoryDTO? CategoryEvent { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        #region Venue
        public string? VenueName { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? PlaceId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WebsiteUrl { get; set; }

        #endregion

        #region media

        public string? ThumbnailUrl { get; set; }
        public string? CoverUrl { get; set; }

        #endregion
    }
}