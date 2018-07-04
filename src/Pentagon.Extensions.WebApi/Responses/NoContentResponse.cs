// -----------------------------------------------------------------------
//  <copyright file="NoContentResponse.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System;

    public class NoContentResponse : IEquatable<NoContentResponse>, INoContentResponse
    {
        public bool IsSuccess { get; set; }

        public Exception Exception { get; set; }

        #region Operators

        public static implicit operator bool(NoContentResponse response) => response.IsSuccess;

        /// <inheritdoc />
        public static bool operator ==(NoContentResponse left, NoContentResponse right) => Equals(left, right);

        /// <inheritdoc />
        public static bool operator !=(NoContentResponse left, NoContentResponse right) => !Equals(left, right);

        #endregion

        #region IEquatable members

        /// <inheritdoc />
        public bool Equals(NoContentResponse other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return IsSuccess == other.IsSuccess && Equals(Exception, other.Exception);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((NoContentResponse) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (IsSuccess.GetHashCode() * 397) ^ (Exception != null ? Exception.GetHashCode() : 0);
            }
        }

        #endregion
    }
}