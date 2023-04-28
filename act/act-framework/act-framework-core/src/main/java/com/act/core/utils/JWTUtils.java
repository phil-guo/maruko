package com.act.core.utils;

import io.jsonwebtoken.Claims;
import io.jsonwebtoken.Jws;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;
import lombok.var;

import java.util.Date;
import java.util.Hashtable;
import java.util.Map;

public class JWTUtils {

    //过期时间
    private static final long EXPIRE_TIME = 86400 * 1000;

    private static final String JWT_KEY = "qwe123QWE";

    //获取Token
    public static String getToken(Map<String, Object> map) {

        var date = new Date(System.currentTimeMillis() + EXPIRE_TIME);

        return Jwts.builder()
                //主题
                .setSubject("Authorization")
                //设置jwt生成时间
                .setIssuedAt(new Date())
                .addClaims(map)
                .setExpiration(date)
                .signWith(SignatureAlgorithm.HS256, JWT_KEY)
                .compact();

    }

    //解析JWT的Token
    public static Map<String, Object> parseClaimsJws(String token) {
        if (token == null)
            return new Hashtable<String, Object>();
        var jwtParser = Jwts.parser();
        jwtParser.setSigningKey(JWT_KEY);
        Jws<Claims> claimsJws = jwtParser.parseClaimsJws(token);
        return claimsJws.getBody();
    }
}
