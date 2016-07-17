<?php
// src/Appbundle/Entity/Account.php
namespace AppBundle\Entity;

use Doctrine\ORM\Mapping as ORM;

/**
* @ORM\Entity
* @ORM\Table(name="account")
*/
class Account
{
    /**
    * @ORM\Column(type="integer")
     * @ORM\Id
     * @ORM\GeneratedValue(strategy="AUTO")
     */
    protected $id;

    /**
    * @ORM\Column(type="string")
     */
    protected $token;

    /**
     * @ORM\OneToOne(targetEntity="Avatar", mappedBy="account")
     */
     private $avatar;

     /**
      * @ORM\OneToOne(targetEntity="Fitbit", mappedBy="account")
      */
      private $fitbit;

    /**
     * Get id
     *
     * @return integer
     */
    public function getId()
    {
        return $this->id;
    }

    /**
     * Set name
     *
     * @param string $name
     *
     * @return Account
     */
    public function setName($name)
    {
        $this->name = $name;

        return $this;
    }

    /**
     * Get name
     *
     * @return string
     */
    public function getName()
    {
        return $this->name;
    }

    /**
     * Set avatar
     *
     * @param \AppBundle\Entity\Avatar $avatar
     *
     * @return Account
     */
    public function setAvatar(\AppBundle\Entity\Avatar $avatar = null)
    {
        $this->avatar = $avatar;

        return $this;
    }

    /**
     * Get avatar
     *
     * @return \AppBundle\Entity\Avatar
     */
    public function getAvatar()
    {
        return $this->avatar;
    }

    /**
     * Set fitbit
     *
     * @param \AppBundle\Entity\Fitbit $fitbit
     *
     * @return Account
     */
    public function setFitbit(\AppBundle\Entity\Fitbit $fitbit = null)
    {
        $this->fitbit = $fitbit;

        return $this;
    }

    /**
     * Get fitbit
     *
     * @return \AppBundle\Entity\Fitbit
     */
    public function getFitbit()
    {
        return $this->fitbit;
    }

    /**
     * Set token
     *
     * @param string $token
     *
     * @return Account
     */
    public function setToken($token)
    {
        $this->token = $token;

        return $this;
    }

    /**
     * Get token
     *
     * @return string
     */
    public function getToken()
    {
        return $this->token;
    }
}
