﻿namespace Coupon.API.Infrastructure.Repositories
{
    using System.Threading.Tasks;
    using Models;

    public interface ICouponRepository
    {
        Task<Coupon> FindCouponByCodeAsync(string code);

        Task UpdateCouponConsumedByCodeAsync(string code, int orderId);

        Task UpdateCouponReleasedByOrderIdAsync(int orderId);
    }
}
