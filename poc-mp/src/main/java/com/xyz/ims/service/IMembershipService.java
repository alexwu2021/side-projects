package com.xyz.ims.service;

import java.util.List;

public interface IMembershipService {

    boolean isMemberOf(int entityIdToQuery, int entityIdQueriedAgainst);
    List<Integer> getChildren(int entityId);
}
