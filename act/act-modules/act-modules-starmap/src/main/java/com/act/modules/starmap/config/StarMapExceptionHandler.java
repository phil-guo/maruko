package com.act.modules.starmap.config;

import com.act.core.handler.GlobalExceptionHandler;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.RestControllerAdvice;

/**
 * 全局异常拦截器
 */
@RestControllerAdvice
@RestController
public class StarMapExceptionHandler extends GlobalExceptionHandler {
}
