﻿namespace Coupon.API.Dtos
{
    using Infrastructure.Models;

    public class Mapper : IMapper<CouponDto, Coupon>
    {
        public CouponDto Translate(Coupon entity)
        {
            return new CouponDto
            {
                Code = entity.Code,
                Discount = entity.Discount
            };
        }
    }
}
