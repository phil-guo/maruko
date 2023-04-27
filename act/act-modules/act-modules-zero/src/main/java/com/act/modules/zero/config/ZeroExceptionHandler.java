package com.act.modules.zero.config;

import com.act.core.handler.GlobalExceptionHandler;
import com.act.core.utils.AjaxResponse;
import com.act.core.utils.FriendlyException;
import lombok.var;
import org.springframework.http.HttpStatus;
import org.springframework.validation.BindException;
import org.springframework.validation.FieldError;
import org.springframework.validation.ObjectError;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.RestControllerAdvice;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

/**
 * 全局异常拦截器
 */
@RestControllerAdvice
@RestController
public class ZeroExceptionHandler extends GlobalExceptionHandler {
}
