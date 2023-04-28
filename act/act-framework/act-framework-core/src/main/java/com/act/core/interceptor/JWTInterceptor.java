package com.act.core.interceptor;


import com.act.core.utils.FriendlyException;
import com.act.core.utils.JWTUtils;
import com.github.benmanes.caffeine.cache.Cache;
import io.jsonwebtoken.ExpiredJwtException;
import io.jsonwebtoken.UnsupportedJwtException;
import lombok.var;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Component;
import org.springframework.web.servlet.HandlerInterceptor;

@Component
@SuppressWarnings("all")
public class JWTInterceptor implements HandlerInterceptor {

    @Autowired
    Cache<String, Object> _cache;

    public boolean preHandle(javax.servlet.http.HttpServletRequest request, javax.servlet.http.HttpServletResponse response, Object handler) throws Exception {
        var method = request.getMethod();
        if ("OPTIONS".equals(method)) {
            return true;
        }

        //获取请求头部信息
        var token = request.getHeader("Authorization").replace("Bearer ", "");
        if (token == null)
            throw new FriendlyException("Authorization 不能为空！", HttpStatus.UNAUTHORIZED.value());

        //验证token
        JWTUtils.parseClaimsJws(token);

        return true;
    }
}
