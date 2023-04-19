package com.act.core.interceptor;


import com.act.core.utils.JWTUtils;
import io.jsonwebtoken.ExpiredJwtException;
import io.jsonwebtoken.UnsupportedJwtException;
import lombok.var;
import org.springframework.web.servlet.HandlerInterceptor;

public class JWTInterceptor implements HandlerInterceptor {

    public boolean preHandle(javax.servlet.http.HttpServletRequest request, javax.servlet.http.HttpServletResponse response, Object handler) throws Exception {
        var method = request.getMethod();
        if ("OPTIONS".equals(method)) {
            return true;
        }

        try {
            //获取请求头部信息
            var token = request.getHeader("token");
            if (token == null)
                return false;

            //验证token
            var result = JWTUtils.parseClaimsJws(token);
            return true;
        } catch (ExpiredJwtException e) {
            //过期
        } catch (UnsupportedJwtException e) {
            //不合法
        } catch (Exception e) {
            //不合法
        }
        return false;
    }
}
