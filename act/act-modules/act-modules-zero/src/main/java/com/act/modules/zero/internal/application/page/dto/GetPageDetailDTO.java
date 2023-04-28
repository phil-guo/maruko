package com.act.modules.zero.internal.application.page.dto;

import com.act.modules.zero.internal.application.pageConfig.dto.PageConfigDTO;
import lombok.Data;

@Data
public class GetPageDetailDTO {
    private PageConfigDTO pageConfigs = new PageConfigDTO();
}
