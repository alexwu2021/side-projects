package com.xyz.ims.repository.dw;


import com.xyz.ims.db.jpa.DimUsersEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface IDimUserEntityRepository extends JpaRepository<DimUsersEntity, Integer> {
}

